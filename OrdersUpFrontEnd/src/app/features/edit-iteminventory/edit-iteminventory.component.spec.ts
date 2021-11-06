import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditIteminventoryComponent } from './edit-iteminventory.component';

describe('EditIteminventoryComponent', () => {
  let component: EditIteminventoryComponent;
  let fixture: ComponentFixture<EditIteminventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditIteminventoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditIteminventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
