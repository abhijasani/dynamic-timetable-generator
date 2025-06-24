import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [ CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'dynamic_timetable-ui';

  step = 1;
  totalHours = 0;
  timetable: string[][] = [];

  configForm!: FormGroup;
  subjectsForm!: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.configForm = this.fb.group({
      workingDays: [5, [Validators.required, Validators.min(1), Validators.max(7)]],
      subjectsPerDay: [4, [Validators.required, Validators.min(1), Validators.max(8)]],
      totalSubjects: [4, [Validators.required, Validators.min(1)]]
    });

    this.subjectsForm = this.fb.group({
      subjects: this.fb.array([])
    });
  }

  get subjectArray() {
    return this.subjectsForm.get('subjects') as FormArray;
  }

  onNext() {
    const config = this.configForm.value;
    this.totalHours = config.workingDays! * config.subjectsPerDay!;

    this.subjectArray.clear();
    for (let i = 0; i < config.totalSubjects!; i++) {
      this.subjectArray.push(
        this.fb.group({
          name: ['Subject ' + (i + 1), Validators.required],
          hours: [0, [Validators.required, Validators.min(1)]]
        })
      );
    }
    this.step = 2;
  }

  onGenerate() {
    const config = this.configForm.value;
    const subjects = this.subjectArray.value.map((s: any) => ({ name: s.name, hours: +s.hours }));
    const subjectTotal = subjects.reduce((acc: number, s: any) => acc + s.hours, 0);

    if (subjectTotal !== this.totalHours) {
      alert(`Total subject hours (${subjectTotal}) must equal total weekly hours (${this.totalHours})`);
      return;
    }

    const payload = {
      configuration: config,
      subjects: subjects.map((s: any) => ({ name: s.name, hours: s.hours }))
    };

    this.http.post<any>('http://localhost:5237/api/timetable/generate', payload)
      .subscribe(
        res => {
          this.timetable = res.timetable;
          this.step = 3;
        },
        err => alert('Failed to generate timetable')
      );
  }
}
