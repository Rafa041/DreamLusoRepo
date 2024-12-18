import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesPurchaseComponent } from './properties-purchase.component';

describe('PropertiesPurchaseComponent', () => {
  let component: PropertiesPurchaseComponent;
  let fixture: ComponentFixture<PropertiesPurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesPurchaseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesPurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
