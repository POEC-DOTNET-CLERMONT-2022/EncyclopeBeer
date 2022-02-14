import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BeerdetailsComponent } from './beerdetails.component';

describe('BeerdetailsComponent', () => {
  let component: BeerdetailsComponent;
  let fixture: ComponentFixture<BeerdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BeerdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BeerdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
