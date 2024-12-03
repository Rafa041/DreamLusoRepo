import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatAgentComponent } from './chat-agent.component';

describe('ChatAgentComponent', () => {
  let component: ChatAgentComponent;
  let fixture: ComponentFixture<ChatAgentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChatAgentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatAgentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
