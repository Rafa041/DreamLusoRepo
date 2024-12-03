import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardSidebarClientComponent } from './dashboard-sidebar-client.component';

describe('DashboardSidebarClientComponent', () => {
  let component: DashboardSidebarClientComponent;
  let fixture: ComponentFixture<DashboardSidebarClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardSidebarClientComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardSidebarClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
