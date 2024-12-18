import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesPortoComponent } from './properties-porto.component';

describe('PropertiesPortoComponent', () => {
  let component: PropertiesPortoComponent;
  let fixture: ComponentFixture<PropertiesPortoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PropertiesPortoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertiesPortoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
