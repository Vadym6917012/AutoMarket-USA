import { ModelService } from './../../../services/model.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Generation } from 'src/app/models/generation/generation';
import { Make } from 'src/app/models/make/make';
import { Model } from 'src/app/models/model/model';
import { Modification } from 'src/app/models/modification/modification';
import { ProducingCountry } from 'src/app/models/producing-country/producing-country';
import { CarService } from 'src/app/services/car.service';
import { GenerationService } from 'src/app/services/generation.service';
import { MakeService } from 'src/app/services/make.service';
import { ModificationService } from 'src/app/services/modification.service';
import { ProducingCountryService } from 'src/app/services/producing-country.service';
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
  makes: Make[] = [];
  models: Model[] = [];
  generations: Generation[] = [];
  modifications: Modification[] = [];

  selectedCountryId: number = 0;
  selectedMakeId: number = 0;
  selectedModelId: number = 0;
  selectedGenerationId?: number = 0;
  selectedModificationId?: number = 0;

  filteredMakes: Make[] = [];
  filteredModels: Model[] = [];
  filteredGenerations: Generation[] = [];
  filteredModifications: Modification[] = [];

  constructor(private carService: CarService,
    private countryServise: ProducingCountryService,
    private makeService: MakeService,
    private generationSerice: GenerationService,
    private modelService: ModelService,
    private modificationService: ModificationService,
    private formBuilder: FormBuilder,
    private sharedService: SharedService,
    private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.getCountries();
    
    this.addCarForm.get('countryId')?.valueChanges.subscribe((countryId) => {

      if (countryId) {
        this.filteredModels = [];
        this.filteredGenerations = [];
        this.filteredModifications = [];
      }

      if (countryId === 0) {
        this.makeService.getMakes().subscribe(data => {
          this.filteredMakes = data;
        });
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

      this.generationSerice.getGenerationByModel(modelId).subscribe(data => {
        this.filteredGenerations = data;
      });
      this.modificationService.getModificationsByModel(modelId).subscribe(data => {
        this.filteredModifications = data;
      });
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
      fuelTypeId: ['', [Validators.required]],
      year: ['', [Validators.required]],
      price: ['', [Validators.required]],
      mileage: ['', [Validators.required]],
      description: ['', [Validators.required]],
    });
  }

  addCar() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.addCarForm.valid) {
      const carData = this.addCarForm.value;

      this.carService.addCar(carData, this.selectedImages).subscribe({
        next: (response: any) => {
          this.sharedService.showNotification(true, response.value.title, response.value.message);

          if (response.id) {
            this.router.navigateByUrl(`/car/car-details/${response.id}`)
          }
          console.log(response);
        },
        error: error => {
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

  getGenerations() {
    this.generationSerice.getGenerations().subscribe(data => {
      this.generations = data;
    })
  }

  getModels() {
    this.modelService.getModels().subscribe(data => {
      this.models = data;
    })
  }

  getModifications() {
    this.modificationService.getModifications().subscribe(data => {
      this.modifications = data;
    })
  }

  onCountrySelect() {
    this.selectedMakeId = 0;
    this.selectedModelId = 0;
    this.selectedGenerationId = 0;
    this.selectedModificationId = 0;

    if (this.selectedCountryId === 0) {
      this.makeService.getMakes().subscribe(data => {
        this.filteredMakes = data;
        console.log(this.filteredMakes);
      });
    } else {
      this.makeService.getMakeByCountry(this.selectedCountryId).subscribe(data => {
        this.filteredMakes = data;
        console.log(this.filteredMakes);
      });
    }
  }

  onModelSelect() {
    this.selectedGenerationId = 0;
    this.selectedModificationId = 0;

    
  }
}
