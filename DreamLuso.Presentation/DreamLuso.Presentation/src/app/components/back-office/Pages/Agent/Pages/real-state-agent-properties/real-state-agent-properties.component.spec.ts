import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealStateAgentPropertiesComponent } from './real-state-agent-properties.component';

describe('RealStateAgentPropertiesComponent', () => {
  let component: RealStateAgentPropertiesComponent;
  let fixture: ComponentFixture<RealStateAgentPropertiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RealStateAgentPropertiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RealStateAgentPropertiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
