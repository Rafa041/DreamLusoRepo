import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealEstateAgentPropertiesComponent } from './real-estate-agent-properties.component';

describe('RealEstateAgentPropertiesComponent', () => {
  let component: RealEstateAgentPropertiesComponent;
  let fixture: ComponentFixture<RealEstateAgentPropertiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RealEstateAgentPropertiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RealEstateAgentPropertiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
