export interface AccountPattern {
    mainAccountId: any;
    dimensions: Dimension[];
}
export interface Dimension {
    id: any;
    name: string,
    selectedValue: string
    dValues: DValues[];
}
export interface DValues {
    dId: any;
    value: string
}
