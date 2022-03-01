import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoritebeersComponent } from './favoritebeers.component';

describe('FavoritebeersComponent', () => {
  let component: FavoritebeersComponent;
  let fixture: ComponentFixture<FavoritebeersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FavoritebeersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoritebeersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
