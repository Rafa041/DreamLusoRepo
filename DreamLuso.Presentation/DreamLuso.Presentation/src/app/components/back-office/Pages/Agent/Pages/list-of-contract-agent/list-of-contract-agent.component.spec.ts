import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOfContractAgentComponent } from './list-of-contract-agent.component';

describe('ListOfContractAgentComponent', () => {
  let component: ListOfContractAgentComponent;
  let fixture: ComponentFixture<ListOfContractAgentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListOfContractAgentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListOfContractAgentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
