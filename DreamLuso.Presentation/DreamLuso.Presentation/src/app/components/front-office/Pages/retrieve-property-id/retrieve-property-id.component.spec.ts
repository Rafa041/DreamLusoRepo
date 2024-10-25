import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RetrievePropertyIdComponent } from './retrieve-property-id.component';

describe('RetrievePropertyIdComponent', () => {
  let component: RetrievePropertyIdComponent;
  let fixture: ComponentFixture<RetrievePropertyIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RetrievePropertyIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RetrievePropertyIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
