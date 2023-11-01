export interface CarAdd {
    id: number;
    modelId: number;
    generationId: number;
    modificationId: number;
    vin: string;
    bodyTypeId: number;
    gearBoxTypeId: number;
    fuelTypeId: number;
    year: number;
    price: number;
    mileage: number;
    description: string;
    userId: string;
}