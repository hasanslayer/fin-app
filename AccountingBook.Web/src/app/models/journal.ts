export interface Journal {
  id: number;
  date: Date;
  referenceNo: string;
  readyForPosting: boolean,
  posted: boolean,
  lines: JournalLine[];
}
export interface JournalLine {
  id: number;
  accountId: number,
  mainAccountId: string
  drCrId: number,
  amount: number,
  memo: string
}