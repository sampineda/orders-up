import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIteminventoryComponent } from './create-iteminventory.component';

describe('CreateIteminventoryComponent', () => {
  let component: CreateIteminventoryComponent;
  let fixture: ComponentFixture<CreateIteminventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateIteminventoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIteminventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
