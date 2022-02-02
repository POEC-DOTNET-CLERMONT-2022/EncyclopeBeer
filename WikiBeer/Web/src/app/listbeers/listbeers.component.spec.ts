import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListbeersComponent } from './listbeers.component';

describe('ListbeersComponent', () => {
  let component: ListbeersComponent;
  let fixture: ComponentFixture<ListbeersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListbeersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListbeersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
