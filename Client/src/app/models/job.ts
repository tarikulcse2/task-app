export interface Job {
    id: number;
    title: string;
    description: string;
    date: Date;
    fromTime: string;
    toTime: string;
    location: string;
    notify: number;
    label: string;
}
