export interface CarAdd {
    id: number;
    modelId: number;
    modificationId: number;
    vin: string;
    bodyTypeId: number;
    gearBoxId: number;
    fuelTypeId: number;
    year: number;
    price: number;
    mileage: number;
    description: string;
    userId: string;
}