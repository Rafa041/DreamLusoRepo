import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RetrieveAllPropertiesComponent } from './retrieve-all-properties.component';

describe('RetrieveAllPropertiesComponent', () => {
  let component: RetrieveAllPropertiesComponent;
  let fixture: ComponentFixture<RetrieveAllPropertiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RetrieveAllPropertiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RetrieveAllPropertiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
