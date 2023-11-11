import { ModelService } from './../../../services/model.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BodyType } from 'src/app/models/body-type/body-type';
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
import { ModificationService } from 'src/app/services/modification.service';
import { ProducingCountryService } from 'src/app/services/producing-country.service';
import { TechnicalConditionService } from 'src/app/services/technical-condition.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  styleUrls: ['./car-add.component.css']
})
export class CarAddComponent implements OnInit {

  addCarForm: FormGroup = new FormGroup({});
  selectedImages: File[] = [];
  errorMessages: string[] = [];
  submitted = false;

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
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.getMakes();
    this.getCountries();
    this.getBodyTypes();
    this.getGearBoxes();
    this.getFuelTypes();
    this.getDriveTrain();
    this.getTechnicalCondition();

    this.addCarForm.get('countryId')?.valueChanges.subscribe((countryId) => {

      if (countryId) {
        this.filteredModels = [];
        this.filteredGenerations = [];
        this.filteredModifications = [];
      }

      if (countryId === 0) {
        this.getMakes();
      } else {
        this.makeService.getMakeByCountry(countryId).subscribe(data => {
          this.filteredMakes = data;
        });
      }
    });

    this.addCarForm.get('makeId')?.valueChanges.subscribe((makeId) => {

      if (makeId) {
        this.filteredModels = [];
        this.filteredGenerations = [];
        this.filteredModifications = [];
      }

      this.modelService.getModelsByMake(makeId).subscribe(data => {
        this.filteredModels = data;
      });
    });

    this.addCarForm.get('modelId')?.valueChanges.subscribe((modelId) => {

      if (modelId) {
        this.filteredGenerations = [];
        this.filteredModifications = [];
      }

      this.generationService.getGenerationByModel(modelId).subscribe(data => {
        this.filteredGenerations = data;
      });
      this.modificationService.getModificationsByModel(modelId).subscribe(data => {
        this.filteredModifications = data;
      });
    });

    this.currentUser.subscribe((user) => {
      if (user) {
        this.addCarForm.patchValue({ userId: user.id });
      }
    });
  }

  onImagesSelected(event: any) {
    this.selectedImages = event.target.files;
  }

  initializeForm() {
    this.addCarForm = this.formBuilder.group({
      countryId: [''],
      makeId: [''],
      modelId: ['', [Validators.required]],
      generationId: ['', [Validators.required]],
      modificationId: ['', [Validators.required]],
      vin: ['', [Validators.required]],
      bodyTypeId: ['', [Validators.required]],
      gearBoxTypeId: ['', [Validators.required]],
      driveTrainId: ['', [Validators.required]],
      technicalConditionId: ['', [Validators.required]],
      fuelTypeId: ['', [Validators.required]],
      year: ['', [Validators.required, this.yearWithinGenerationRangeValidator()]],
      price: ['', [Validators.required]],
      mileage: ['', [Validators.required]],
      description: ['', [Validators.required]],
      userId: [''],
    });
  }

  yearWithinGenerationRangeValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const year = control.value;

      const generationId = this.addCarForm.get('generationId')?.value;

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


  addCar() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.addCarForm.valid) {
      const carData = this.addCarForm.value;

      this.carService.addCar(carData, this.selectedImages).subscribe({
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

  getCountries() {
    this.countryServise.getProducingCountries().subscribe(data => {
      this.countries = data;
    })
  }

  getMakes() {
    this.makeService.getMakes().subscribe(data => {
      this.filteredMakes = data;
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
