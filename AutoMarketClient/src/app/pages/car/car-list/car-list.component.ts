import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BodyType } from 'src/app/models/body-type/body-type';
import { DriveTrain } from 'src/app/models/drive-train/drive-train';
import { FuelType } from 'src/app/models/fuel-type/fuel-type';
import { GearBox } from 'src/app/models/gearbox/gearbox';
import { Generation } from 'src/app/models/generation/generation';
import { Make } from 'src/app/models/make/make';
import { Model } from 'src/app/models/model/model';
import { Modification } from 'src/app/models/modification/modification';
import { TechnicalCondition } from 'src/app/models/technical-condition/technical-condition';
import { BodyTypeService } from 'src/app/services/body-type.service';
import { CarService } from 'src/app/services/car.service';
import { DriveTrainService } from 'src/app/services/drive-train.service';
import { FuelTypeService } from 'src/app/services/fuel-type.service';
import { GearBoxTypeService } from 'src/app/services/gear-box-type.service';
import { GenerationService } from 'src/app/services/generation.service';
import { MakeService } from 'src/app/services/make.service';
import { ModelService } from 'src/app/services/model.service';
import { ModificationService } from 'src/app/services/modification.service';
import { TechnicalConditionService } from 'src/app/services/technical-condition.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {

  carInfo: any | null = null;
  makes: Make[] = [];
  bodytypes: BodyType[] = [];
  gearboxes: GearBox[] = [];
  fueltypes: FuelType[] = [];
  drivetrains: DriveTrain[] = [];
  technicalconditions: TechnicalCondition[] = [];

  filteredModels: Model[] = [];
  filteredGenerations: Generation[] = [];
  filteredModifications: Modification[] = [];

  isListView = true;
  selectedSortOption = '';

  carFilter: FormGroup = new FormGroup({});

  constructor(private carService: CarService,
    private makeService: MakeService,
    private generationService: GenerationService,
    private modelService: ModelService,
    private modificationService: ModificationService,
    private bodyTypeService: BodyTypeService,
    private gearBoxService: GearBoxTypeService,
    private fuelTypeService: FuelTypeService,
    private driveTrainService: DriveTrainService,
    private technicalConditionService: TechnicalConditionService,
    private formBuilder: FormBuilder) {
      
    const navigation = window.history.state;

    if (navigation && navigation.cars) {
      this.carInfo = navigation.cars;
    } else {
      this.showCars();
    }
  }

  ngOnInit(): void {
    this.initializeForm();
    this.getMakes();
    this.getBodyTypes();
    this.getGearBoxes();
    this.getFuelTypes();
    this.getDriveTrain();
    this.getTechnicalCondition();

    this.carFilter.get('makeId')?.valueChanges.subscribe((makeId) => {
      this.modelService.getModelsByMake(makeId).subscribe(data => {
        this.filteredModels = data;
      })
    });

    this.carFilter.get('modelId')?.valueChanges.subscribe((modelId) => {

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
  }

  initializeForm() {
    this.carFilter = this.formBuilder.group({
      makeId: [''],
      modelId: [''],
      generationId: [''],
      modificationId: [''],
      bodyTypeId: [''],
      gearBoxTypeId: [''],
      driveTrainId: [''],
      technicalConditionId: [''],
      fuelTypeId: [''],
      yearFrom: [''],
      yearTo: [''],
      mileageFrom: [''],
      mileageTo: [''],
      priceFrom: [''],
      priceTo: [''],
    });
  }

  sortCars() {
    if (this.selectedSortOption === 'default') {
      this.showCars();
    } else if (this.selectedSortOption === 'asc') {
      this.carInfo.sort((a: { price: number }, b: { price: number }) => a.price - b.price);
    } else if (this.selectedSortOption === 'desc') {
      this.carInfo.sort((a: { price: number }, b: { price: number }) => b.price - a.price);
    }
  }

  showCars() {
    this.carService.getCars().subscribe((data) => {
      this.carInfo = data;
    });
  }

  toggleView(view: 'list' | 'grid') {
    if (view === 'list') {
      this.isListView = true;
    } else if (view === 'grid') {
      this.isListView = false;
    }
  }

  getMakes() {
    this.makeService.getMakes().subscribe((data) => {
      this.makes = data;
    });
  }

  getModels() {
    this.modelService.getModels().subscribe((data) => {
      this.filteredModels = data;
    });
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

  filter() {
    const filterValue = this.carFilter.value;
    this.carService.carFilter(filterValue).subscribe((data) => {
      this.carInfo = data;
    });
  }
}
