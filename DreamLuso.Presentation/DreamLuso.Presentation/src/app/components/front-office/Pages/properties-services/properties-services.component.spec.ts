import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesServicesComponent } from './properties-services.component';

describe('PropertiesServicesComponent', () => {
  let component: PropertiesServicesComponent;
  let fixture: ComponentFixture<PropertiesServicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesServicesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
