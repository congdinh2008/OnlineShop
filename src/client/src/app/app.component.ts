import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { uuid } from 'uuid';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { faUserInjured } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public title: string = 'Online Shop';
  private url: string = 'https://localhost:5001/api/categories';

  public categories!: any;
  public isExpanded: boolean = false;
  public isDropdowMenu: boolean = false;

  public createCategoryForm!: FormGroup;
  public nameControl!: FormControl;
  public notesControl!: FormControl;

  /**
   *
   */
  constructor(private httpClient: HttpClient, private fb: FormBuilder) {
    this.getCategories().subscribe((x) => {
      this.categories = x;
    });
  }

  ngOnInit() {
    this.createForm();
  }

  private createForm() {
    this.nameControl = new FormControl('', Validators.required);
    this.notesControl = new FormControl('');

    this.createCategoryForm = this.fb.group({
      name: this.nameControl,
      notes: this.notesControl,
    });
  }

  private getCategories() {
    return this.httpClient.get(this.url);
  }

  public expandMenu() {
    this.isExpanded = !this.isExpanded;
  }

  public expandDropdownMenu() {
    this.isDropdowMenu = !this.isDropdowMenu;
  }

  public removeCategory(i: number) {
    this.categories.splice(i, 1);
  }

  public submit() {
    if (this.createCategoryForm.invalid) {
    }

    let params = Object.assign({}, this.createCategoryForm.value);

    params = Object.assign({}, params, {
      id: uuid(),
      isDeleted: false
    });

    this.httpClient.post(this.url, params).subscribe((x) => {
      console.log(x);
    });
  }
}
