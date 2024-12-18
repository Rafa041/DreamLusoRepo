import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesLisbonComponent } from './properties-lisbon.component';

describe('PropertiesLisbonComponent', () => {
  let component: PropertiesLisbonComponent;
  let fixture: ComponentFixture<PropertiesLisbonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesLisbonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesLisbonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
