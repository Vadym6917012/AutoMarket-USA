import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Generation } from 'src/app/models/generation/generation';
import { Model } from 'src/app/models/model/model';
import { GenerationService } from 'src/app/services/generation.service';
import { ModelService } from 'src/app/services/model.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-generation',
  templateUrl: './add-edit-generation.component.html',
  styleUrls: ['./add-edit-generation.component.css']
})
export class AddEditGenerationComponent implements OnInit {

  generationForm: FormGroup = new FormGroup({})
  formInitialized = false;
  id!: number;
  addNew = true;
  submitted = false;
  errorMessages: string[] = [];

  models: Model[] = [];

  constructor(private generationService: GenerationService,
    private modelService: ModelService,
    private sharedService: SharedService,
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute) {}
  
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.id = params['id'];
    });
    if (this.id) {
      this.addNew = false; // this means we are editing a member
      this.getGeneration(this.id);
    } else {
      this.initializeForm(undefined);
    }
    
    this.getModels();
  }

  getGeneration(id: number) {
    this.generationService.getGeneration(id).subscribe({
      next: generation => {
        this.initializeForm(generation);
      }
    })
  }

  getModels() {
    this.modelService.getModels().subscribe({
      next: models => {
        this.models = models;
      }
    })
  }

  initializeForm(generation: Generation | undefined) {
    if (generation) {
      this.generationForm = this.formBuilder.group({
        id: [generation.id],
        name: [generation.name, Validators.required],
        yearFrom: [generation.yearFrom, Validators.required],
        yearTo: [generation.yearTo, Validators.required],
      });
    } else {
      // form for creating a member
      this.generationForm = this.formBuilder.group({
        modelId: ['', Validators.required],
        id: [''],
        name: ['', Validators.required],
        yearFrom: ['', Validators.required],
        yearTo: ['', Validators.required],
      });
    }

    this.formInitialized = true;
  }

  submit() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.generationForm.valid) {
      const modelId = this.generationForm.get('modelId')?.value;
      const generation = this.generationForm.value;
      console.log(this.generationForm.valid)

      this.generationService.addGenerationToModel(modelId, generation).subscribe({
        next: (response: any) => {
          this.sharedService.showNotification(true, response.value.titile, response.value.message);
          this.router.navigateByUrl('/admin');
        },
        error: error => {
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
          } else {
            this.errorMessages.push(error.error);
          }
        }
      })
    }
  }

}
