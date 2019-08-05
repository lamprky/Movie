import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { FormOptionsService } from 'src/app/modules/services/form-options.service';
import { SelectOption } from 'src/app/modules/models/SelectOption';
import { Detail } from 'src/app/modules/models/detail';

@Component({
  selector: 'app-translations',
  templateUrl: './translations.component.html',
  styleUrls: ['./translations.component.scss']
})
export class TranslationsComponent implements OnInit, OnChanges {

  @Input() translations: Detail[];

  public translation: Detail = {
    title: "",
    name: "",
    id: "",
    description: "",
    languageId: "",
    checked: false
  };

  public form: FormGroup;
  languageOptions: SelectOption[];

  constructor(private formOptionsService: FormOptionsService) {
    this.buildForm();
    this.languageOptions = this.formOptionsService.getLanguageOptions();
  }

  ngOnInit() {
    this.buildForm();
  }
  ngOnChanges() {
  }


  buildForm() {
    this.form = new FormGroup({
      title: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      languageId: new FormControl(null, Validators.required),
    });
  }

  onSubmit() {
    Object.keys(this.form.value).map(key => {
      this.translation[key] = this.form.value[key];
    });

    this.translations.push(this.translation)
  }

  canSubmitForm(form: FormGroup) {
    return form.valid;
  }
}
