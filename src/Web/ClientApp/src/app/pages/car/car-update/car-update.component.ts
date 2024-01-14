import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BodyType } from 'src/app/models/body-type/body-type';
import { CarAdd } from 'src/app/models/car/car-add';
import { DriveTrain } from 'src/app/models/drive-train/drive-train';
import { FuelType } from 'src/app/models/fuel-type/fuel-type';
import { GearBox } from 'src/app/models/gearbox/gearbox';
import { Generation } from 'src/app/models/generation/generation';
import { Make } from 'src/app/models/make/make';
import { Model } from 'src/app/models/model/model';
import { Modification } from 'src/app/models/modification/modification';
import { ProducingCountry } from 'src/app/models/producing-country/producing-country';
import { TechnicalCondition } from 'src/app/models/technical-condition/technical-condition';
import { AccountService } from 'src/app/services/account.service';
import { BodyTypeService } from 'src/app/services/body-type.service';
import { CarService } from 'src/app/services/car.service';
import { DriveTrainService } from 'src/app/services/drive-train.service';
import { FuelTypeService } from 'src/app/services/fuel-type.service';
import { GearBoxTypeService } from 'src/app/services/gear-box-type.service';
import { GenerationService } from 'src/app/services/generation.service';
import { MakeService } from 'src/app/services/make.service';
import { ModelService } from 'src/app/services/model.service';
import { ModificationService } from 'src/app/services/modification.service';
import { ProducingCountryService } from 'src/app/services/producing-country.service';
import { TechnicalConditionService } from 'src/app/services/technical-condition.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-car-update',
  templateUrl: './car-update.component.html',
  styleUrls: ['./car-update.component.css']
})
export class CarUpdateComponent implements OnInit {
  updateCarForm: FormGroup = new FormGroup({});
  carId!: number;
  selectedImages: File[] = [];
  errorMessages: string[] = [];
  submitted = false;

  disableSelect: boolean = true;

  countries: ProducingCountry[] = [];
  bodytypes: BodyType[] = [];
  gearboxes: GearBox[] = [];
  fueltypes: FuelType[] = [];
  drivetrains: DriveTrain[] = [];
  technicalconditions: TechnicalCondition[] = [];

  filteredMakes: Make[] = [];
  filteredModels: Model[] = [];
  filteredGenerations: Generation[] = [];
  filteredModifications: Modification[] = [];

  currentUser = this.accountService.user$;

  constructor(private carService: CarService,
    private countryServise: ProducingCountryService,
    private makeService: MakeService,
    private generationService: GenerationService,
    private modelService: ModelService,
    private modificationService: ModificationService,
    private bodyTypeService: BodyTypeService,
    private gearBoxService: GearBoxTypeService,
    private fuelTypeService: FuelTypeService,
    private driveTrainService: DriveTrainService,
    private technicalConditionService: TechnicalConditionService,
    private formBuilder: FormBuilder,
    private sharedService: SharedService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.carId = params['id'];
      console.log('Car ID:', this.carId);
    });

    this.carService.getCarForUpdate(this.carId).subscribe((carData: any) => {
        this.updateCarForm.patchValue(carData);
    });

    this.initializeFormWithData();

    this.getModels();
    this.getModifications();
    this.getGenerations();
    this.getBodyTypes();
    this.getGearBoxes();
    this.getFuelTypes();
    this.getDriveTrain();
    this.getTechnicalCondition();
  }

  onImagesSelected(event: any) {
    this.selectedImages = event.target.files;
  }

  initializeFormWithData() {
    console.log('Before setting form values:', this.updateCarForm.value);

    this.updateCarForm = this.formBuilder.group({
      id: [null],
      modelId: [null, [Validators.required]],
      generationId: [null, [Validators.required]],
      modificationId: [null],
      vin: [null, [Validators.required]],
      bodyTypeId: [null, [Validators.required]],
      gearBoxTypeId: [null, [Validators.required]],
      driveTrainId: [null, [Validators.required]],
      technicalConditionId: [null, [Validators.required]],
      fuelTypeId: [null, [Validators.required]],
      year: [null, [Validators.required, this.yearWithinGenerationRangeValidator()]],
      price: [null, [Validators.required]],
      mileage: [null, [Validators.required]],
      description: [null, [Validators.required]],
      userId: [null]
    });

    console.log('After setting form values:', this.updateCarForm.value);
  }

  yearWithinGenerationRangeValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const year = control.value;

      const generationId = this.updateCarForm.get('generationId')?.value;

      const selectedGeneration = this.filteredGenerations.find(generation => generation.id == generationId);

      if (selectedGeneration) {
        const generationYearFrom = selectedGeneration.yearFrom;
        const generationYearTo = selectedGeneration.yearTo;

        if (year < generationYearFrom || year > generationYearTo) {
          return { yearNotInValidRange: 'Рік не входить у дійсний діапазон для вибраного покоління.' };
        }
      }
      return null;
    };
  }


  updateCar() {
    this.submitted = true;
    this.errorMessages = [];

    console.log(this.updateCarForm.value);
    if (this.updateCarForm.valid) {
      const carData = this.updateCarForm.value;

      this.carService.updateCar(carData).subscribe({
        next: (response: any) => {
          this.sharedService.showNotification(true, response.value.title, response.value.message);
          if (response.value.id) {
            this.router.navigateByUrl(`/car/car-details/${response.value.id}`)
          }
          console.log(response);
        },
        error: error => {
          console.log('Error object:', error);
          if (error.error.error) {
            this.errorMessages = error.error.error;
          } else {
            this.errorMessages.push(error.error)
          }
        }
      })
    }
  }

  getModels() {
    this.modelService.getModels().subscribe(data => {
      this.filteredModels = data;
    })
  }

  getModifications() {
    this.modificationService.getModifications().subscribe(data => {
      this.filteredModifications = data;
    })
  }

  getGenerations() {
    this.generationService.getGenerations().subscribe(data => {
      this.filteredGenerations = data;
    })
  }

  getBodyTypes() {
    this.bodyTypeService.getBodyTypes().subscribe(data => {
      this.bodytypes = data;
    })
  }

  getGearBoxes() {
    this.gearBoxService.getGearBoxes().subscribe(data => {
      this.gearboxes = data;
    })
  }

  getFuelTypes() {
    this.fuelTypeService.getFuelTypes().subscribe(data => {
      this.fueltypes = data;
    })
  }

  getDriveTrain() {
    this.driveTrainService.getDriveTrains().subscribe(data => {
      this.drivetrains = data;
    })
  }

  getTechnicalCondition() {
    this.technicalConditionService.getTechnicalConditions().subscribe(data => {
      this.technicalconditions = data;
    })
  }
}