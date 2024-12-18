import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesRentComponent } from './properties-rent.component';

describe('PropertiesRentComponent', () => {
  let component: PropertiesRentComponent;
  let fixture: ComponentFixture<PropertiesRentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesRentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
