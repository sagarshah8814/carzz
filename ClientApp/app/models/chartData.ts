﻿export interface ChartData {
    labels: string[];
    datasets:[{
        data: number[];
        backgroundColor:string[];
    }];
}