export interface ExchangeRateRowDto {
    currency: string;
    rate: number;
    previousRate?: number;
    change?: number;
}

export interface ExchangeRateDto {
    baseCurrency: string;
    conversionRates: ExchangeRateRowDto[];
}
