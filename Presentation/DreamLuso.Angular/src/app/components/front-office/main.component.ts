import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  template: '<app-header></app-header><main><router-outlet></router-outlet></main>',
})
export class MainComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
