export interface Car {
    id: number;
    makeName: string;
    modelName: string;
    generationName: string;
    modificationName: string;
    vin: string;
    bodyTypeName: string;
    gearBoxTypeName: string;
    driveTrainName: string;
    technicalConditionName: string;
    fuelTypeName: string;
    year: number;
    price: number;
    mileage: number;
    description: string;
    userEmail: string;
    firstName: string;
    lastName: string;
    userPhoneNumber: string;
    imagesPath: string[];
    isAdvertisementApproved: boolean;
    dateCreated: Date;
}