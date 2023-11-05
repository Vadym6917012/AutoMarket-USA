export interface CarFilter {
    makeId: number;
    modelId: number;
    generationId: number;
    modificationId: number;
    bodyTypeId: number;
    gearBoxTypeId: number;
    driveTrainId: number;
    technicalConditionId: number;
    fuelTypeId: number;
    yearFrom: number;
    yearTo: number;
    mileageFrom: number;
    mileageTo: number;
    priceFrom: number;
    priceTo: number;
}