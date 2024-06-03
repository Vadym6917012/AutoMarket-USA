import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Make } from 'src/app/models/make/make';
import { Model } from 'src/app/models/model/model';
import { MakeService } from 'src/app/services/make.service';
import { ModelService } from 'src/app/services/model.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-model',
  templateUrl: './add-edit-model.component.html',
  styleUrls: ['./add-edit-model.component.css']
})
export class AddEditModelComponent implements OnInit {

  modelForm: FormGroup = new FormGroup({})
  formInitialized = false;
  id!: number;
  addNew = true;
  submitted = false;
  errorMessages: string[] = [];

  makes: Make[] = [];

  constructor(private modelService: ModelService,
    private makeService: MakeService,
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
      this.getModel(this.id);
    } else {
      this.initializeForm(undefined);
    }
    
    this.getMakes();
  }

  getModel(id: number) {
    this.modelService.getModel(id).subscribe({
      next: model => {
        this.initializeForm(model);
      }
    })
  }

  getMakes() {
    this.makeService.getMakes().subscribe({
      next: makes => {
        this.makes = makes;
      }
    })
  }

  initializeForm(model: Model | undefined) {
    if (model) {
      this.modelForm = this.formBuilder.group({
        id: [model.id],
        name: [model.name, Validators.required],
        makeId: [model.makeId, Validators.required],
      });
    } else {
      // form for creating a model
      this.modelForm = this.formBuilder.group({
        id: [''],
        name: ['', Validators.required],
        makeId: ['', Validators.required],
      });
    }

    this.formInitialized = true;
  }

  submit() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.modelForm.valid) {
      const model = this.modelForm.value;
      console.log(this.modelForm.valid)

      this.modelService.addModel(model).subscribe({
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
