import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardSidebarAgentComponent } from './dashboard-sidebar-agent.component';

describe('DashboardSidebarAgentComponent', () => {
  let component: DashboardSidebarAgentComponent;
  let fixture: ComponentFixture<DashboardSidebarAgentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardSidebarAgentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardSidebarAgentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
