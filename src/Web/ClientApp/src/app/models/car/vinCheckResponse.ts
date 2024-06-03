export interface VinCheckResponce {
    vin: string;
    country: string;
    manufacturer: string;
    model: string;
    class: string;
    region: string;
    wmi: string;
    vds: string;
    vis: string;
    year: number;
    message: string;
    isFound: boolean;
}