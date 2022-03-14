import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrucksDetailsComponent } from './trucks-details.component';

describe('TrucksDetailsComponent', () => {
  let component: TrucksDetailsComponent;
  let fixture: ComponentFixture<TrucksDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrucksDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrucksDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
