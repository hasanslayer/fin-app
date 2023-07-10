import { AccountPattern, DValues, Dimension } from './../models/account-pattern';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Journal } from '../models/journal';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';
import { MenuItem, MessageService } from 'primeng/api';

@Component({
  selector: 'journal-form',
  templateUrl: './journal-form.component.html',
  styleUrls: ['./journal-form.component.css']
})
export class JournalFormComponent implements OnInit {

  accountTemplateText: string = '';
  accountTemplateValue: string = '';

  operationText = 'Insert';
  journalEntry: Journal;
  JournalEntryForm: FormGroup;
  mainAccountId: any;
  dimensionId: any;
  accountPatternArray: AccountPattern[] = [];
  accountPattern: AccountPattern = {
    mainAccountId: '',
    dimensions: []
  };
  dimensionItem: Dimension = {
    id: '',
    name: '',
    selectedValue: '',
    dValues: []
  };
  dValue: DValues = {
    dId: '',
    value: ''
  }

  dimensionMap: Map<string, string> = new Map<string, string>();
  keyValArr: string[] = [];
  selectedValues: string[] = [];
  //drop-downs
  accounts: any = [];
  accountValue: string = '';
  accountValues: any[] = [];
  isShowAccountButtonClick = false;
  isAccountClicked = false
  accountConfigs: any = [];
  accountConfigItem: any = {};
  dimensionValues: any = [];
  items: MenuItem[] = [];
  singleItem: MenuItem = {
    items: this.items
  };
  concatenatedValues = '';
  mainAccounts: MenuItem[] = [];
  singleMainAccount: MenuItem = {};
  childItems: any[] = [];
  drcr: any = [
    { id: 1, name: 'Debit' },
    { id: 2, name: 'Credit' }
  ]

  //get-only property
  get lines(): FormArray {
    return <FormArray>this.JournalEntryForm.get("lines")
  }

  constructor(private router: Router, private route: ActivatedRoute,
    private fb: FormBuilder,
    public apiService: ApiService,
    private messageService: MessageService) { }

  ngOnInit(): void {

    //data-model
    this.journalEntry = {
      id: 0,
      date: new Date(),
      referenceNo: '',
      posted: false,
      readyForPosting: false,
      lines: []
    };

    //form-model
    this.JournalEntryForm = this.fb.group({
      id: 0,
      date: [this.journalEntry.date, [Validators.required]],
      referenceNo: [this.journalEntry.referenceNo],
      posted: this.journalEntry.posted,
      lines: this.fb.array([])
      //lines: this.fb.array([this.buildLine()])
    })

    const id = this.route.snapshot.params['id'];

    //get accounts-data from server
    this.initiateAccountValues(id);

    this.initiateMainAccount();
    this.initiateDimension();
    console.log('accountPattern', this.accountPattern);
  }

  initiateAccountValues(id) {
    this.accountValues = [];
    this.apiService.getPostingAccounts().subscribe((res: any) => {
      this.accounts = res;

      res.forEach(element => {
        this.accountValues.push(element.accountCode)
      });

      if (id !== '0') {
        this.getJournal(id);
      }
    });
  }

  initiateMainAccount() {
    this.accountPatternArray = [];
    this.apiService.getMainAccountConfigs().subscribe((res: any) => {
      this.accountConfigs = res;
      console.log('accountConfigs', this.accountConfigs)
      for (let i = 0; i < this.accountConfigs.length; i++) {
        this.singleMainAccount = {
          id: this.accountConfigs[i].id,
          label: this.accountConfigs[i].patternValue
        }
        this.mainAccounts.push(this.singleMainAccount);
      }
      this.mainAccountId = this.accountConfigs[0].id;
      this.accountPattern.mainAccountId = this.mainAccountId;
      this.accountPatternArray.push(this.accountPattern);

      console.log('mainAccountId', this.mainAccountId);
    })


  }

  initiateDimension() {
    this.items = [];
    this.apiService.getMainAccountConfigs().subscribe((res: any) => {
      this.accountConfigs = res;

      for (let j = 0; j < this.accountConfigs[0].accountConfigsDtos.length; j++) {
        const config = this.accountConfigs[0].accountConfigsDtos[j];
        this.singleItem = {
          label: config.dimensionName,
          id: config.dimensionId,
          command: (event) => {
            this.getDimensionValues(event.item.id);
          }
        }
        this.items.push(this.singleItem);
        this.dimensionItem = {
          id: config.dimensionId,
          name: config.dimensionName,
          selectedValue: '',
          dValues: []
        };
        this.accountPattern.dimensions.push(this.dimensionItem)
      }


      this.singleItem = {
        label: 'MainAccount',
        command: () => {

        }
      };
      this.items.unshift(this.singleItem);
      this.dimensionItem = { // for Main account
        id: '',
        name: 'MainAccount',
        selectedValue: '',
        dValues: []
      };

      this.accountPattern.dimensions.unshift(this.dimensionItem);
    })

    console.log('accountPattern in initiateDimension', this.accountPattern);
  }

  getValueOfClickedItem(value: any) {
    this.concatenatedValues = '';
    debugger;
    const val = value.currentTarget.children[0].innerHTML;
    console.log(val);
    console.log(this.concatenatedValues);
    console.log('clickedDimension', this.dimensionId);
    let currentAccountPattern = this.accountPatternArray.find(a => a.mainAccountId == this.mainAccountId);
    var dimension = currentAccountPattern.dimensions.find(d => d.id == this.dimensionId);
    let keyFound = this.dimensionMap.get(this.dimensionId);
    if (keyFound == undefined && this.dimensionId != undefined) {
      this.dimensionMap.set(dimension.name, val);
    }
    if (dimension != undefined) {
      dimension.selectedValue = val; // to set in input under dimension name
      var selectedDimensionValue = dimension.dValues.find(dv => dv.value == val);
      dimension.dValues = [];
      if (selectedDimensionValue != undefined) {
        dimension.dValues.push(selectedDimensionValue);
      } else {
        this.dValue = {
          dId: this.dimensionId,
          value: val
        },
          dimension.dValues.push(this.dValue);
      }



      console.log('edited dimension', dimension);
      console.log('edited account pattern', currentAccountPattern);
    } else {
      this.accountValue = val;
    }

    this.configAccountValue();


  }
  getMainAccountConfigById(id: any) {
    debugger;
    this.items = [];
    this.childItems = [];
    this.accountValues = [];

    this.accountTemplateText = '';
    this.accountTemplateValue = '';
    // this.accountValue = '';
    this.keyValArr = [];
    this.selectedValues = [];
    this.dimensionMap = new Map<string, string>();

    this.mainAccountId = id;
    this.apiService.getMainAccountConfigsById(id).subscribe((res: any) => {
      this.accountConfigItem = res;
      this.accountPattern = {
        mainAccountId: this.mainAccountId,
        dimensions: [] = []
      };
      for (let j = 0; j < this.accountConfigItem.accountConfigsDtos.length; j++) {
        const config = this.accountConfigItem.accountConfigsDtos[j];
        this.dimensionItem = {
          id: config.dimensionId,
          name: config.dimensionName,
          selectedValue: '',
          dValues: []
        };
        this.accountPattern.dimensions.push(this.dimensionItem);

        this.singleItem = {
          label: config.dimensionName,
          id: config.dimensionId,
          command: (event) => {
            this.getDimensionValues(event.item.id);
          }
        }
        this.items.push(this.singleItem);
      }

      this.accountPatternArray.push(this.accountPattern);


      this.singleItem = {
        label: 'MainAccount',
        command: () => {
          this.initiateAccountValues(0)
        }
      };
      this.items.unshift(this.singleItem);

      this.dimensionItem = { // for Main account
        id: '',
        name: 'MainAccount',
        selectedValue: '',
        dValues: []
      };

      this.accountPattern.dimensions.unshift(this.dimensionItem);
    })

    console.log('mainAccountId', this.mainAccountId);
    console.log('dimensionId', this.dimensionId);
  }

  configAccountValue() {
    this.concatenatedValues = '';
    this.keyValArr = [];
    this.selectedValues = [];
    // let accountKeyVal = this.concatenatedValues + 'account=' + this.accountValue;
    // this.keyValArr.push(accountKeyVal);
    // this.selectedValues.push(this.accountValue);

    let currentAccountPattern = this.accountPatternArray.find(a => a.mainAccountId == this.mainAccountId);

    currentAccountPattern.dimensions.forEach(element => {
      let isFoundInMap = this.dimensionMap.get(element.name);
      if (isFoundInMap != undefined) {
        let keyValue = element.name + '=' + this.dimensionMap.get(element.name);
        this.keyValArr.push(keyValue);
        this.selectedValues.push(this.dimensionMap.get(element.name))
        this.concatenatedValues = this.concatenatedValues + keyValue;
      }
      // else {
      //   let keyValue = element.name + '=' + "";
      //   this.keyValArr.push(keyValue);
      //   this.selectedValues.push("")
      //   this.concatenatedValues = this.concatenatedValues + keyValue;
      // }
    });
    console.log(this.keyValArr.join(','));
    this.accountTemplateValue = this.keyValArr.join(',');
    let result = this.selectedValues.join('-');
    console.log('final-result : ', result)
    this.accountTemplateText = result;
    this.accountPatternArray.forEach(accountPattern => {
      console.log('account pattern', accountPattern);
    });
  }

  getDimensionValues(dimensionId: any) {
    debugger;
    console.log('dimensionId', dimensionId);
    this.dimensionId = dimensionId;
    this.isAccountClicked = false;
    this.childItems = [];
    if (dimensionId != undefined && dimensionId != "") {
      this.apiService.getSelectedValuesByDimensionId(dimensionId).subscribe((res: any) => {
        this.dimensionValues = res;
        let currentAccountPattern = this.accountPatternArray.find(a => a.mainAccountId == this.mainAccountId);
        let dimensionInAccountPattern = currentAccountPattern.dimensions.find(d => d.id == dimensionId);
        dimensionInAccountPattern.dValues = [];
        for (let i = 0; i < this.dimensionValues.length; i++) {
          const dimensionValue = this.dimensionValues[i];
          this.childItems.push(dimensionValue);
          this.dValue = {
            dId: dimensionId,
            value: dimensionValue.value
          };
          dimensionInAccountPattern.dValues.push(this.dValue);
          dimensionInAccountPattern.id = dimensionId;
          dimensionInAccountPattern.name = dimensionInAccountPattern.name;
        }
        console.log(this.childItems);
        console.log('currentAccountPattern in getDimensionValues', currentAccountPattern);
      });
    } else {
      this.changeIsAccountClicked();
      this.initiateAccountValues(0);
    }



  }



  showAccountDetails() {
    this.isShowAccountButtonClick = !this.isShowAccountButtonClick;
  }
  changeIsAccountClicked() {
    this.isAccountClicked = true;
  }

  getJournal(id: number) {
    console.log('loading journal: ', id);

    this.apiService.getJournal(id).subscribe((res: any) => {

      if (this.JournalEntryForm) {
        this.JournalEntryForm.reset();
      }
      //update data-model
      this.journalEntry = res;

      console.log('loaded journal:', this.journalEntry);

      //update form-model
      this.JournalEntryForm.patchValue({
        id: res.id,
        date: new Date(res.date),
        referenceNo: res.referenceNo,
        posted: res.posted
      });

      //lines mapping
      this.journalEntry.lines.map(l => {
        this.addGroupLine(l);
      });

      if (this.journalEntry.posted) {
        //disable on run-time
        //this.JournalEntryForm.get('lines').disable({ onlySelf: true });
        this.operationText = 'Read-Only';
      } else {
        this.operationText = 'Edit';
      }

    });

  }

  backToList() {
    this.router.navigate(['/journal']);
  }

  saveEntry() {

    console.log('dataToSafe: ', this.JournalEntryForm.value)

    this.apiService.saveJournal(this.JournalEntryForm.value).subscribe(res => {
      this.backToList();
    }, err => {
      console.error(err);
      this.messageService.add({ severity: 'error', summary: 'Error Message', detail: err.error });
    })

  }


  //Line Actions

  addLine(): void {
    this.lines.push(this.buildLine());
  }

  private buildLine(): FormGroup {
    return this.fb.group({
      id: 0,
      accountId: this.accounts[0].id,
      mainAccountId: this.mainAccounts[0].id,
      drCrId: this.drcr[0].id,
      amount: 1,
      memo: ''
    })
  }

  deleteLine(index: number): void {

    if (this.lines.length == 1) {
      this.lines.clear();
      //if we want to keep one-line atleast all the time.
      //this.addLine();

    } else {
      this.lines.removeAt(index);
    }
    this.lines.markAsDirty();
  }


  //Helper Methods

  private addGroupLine(line) {
    this.lines.push(this.buildGroupLineFromLine(line))
  }
  private buildGroupLineFromLine(line): FormGroup {
    return this.fb.group({
      id: line.id,
      accountId: line.accountId,
      mainAccountId: line.mainAccountId,
      drCrId: line.drCrId,
      amount: line.amount,
      memo: line.memo
    })
  }




}
