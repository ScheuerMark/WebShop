import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent implements OnInit {
  createProductForm!: FormGroup;
  selectedFile!: File;
  constructor(private formBuilder: FormBuilder, private productService: ProductService) {}

  ngOnInit(): void {
    this.createProductForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ['', Validators.required],
      description: ['', Validators.required],
      image: ['', Validators.required]
    });
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('name', this.createProductForm.get('name')!.value);
    formData.append('price', this.createProductForm.get('price')!.value);
    formData.append('description', this.createProductForm.get('description')!.value);
    formData.append('image', this.selectedFile);

    this.productService.createProduct(formData).subscribe(() => {
      this.createProductForm.reset();
    });

  }
}
