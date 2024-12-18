import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesAlgarveComponent } from './properties-algarve.component';

describe('PropertiesAlgarveComponent', () => {
  let component: PropertiesAlgarveComponent;
  let fixture: ComponentFixture<PropertiesAlgarveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesAlgarveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesAlgarveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
