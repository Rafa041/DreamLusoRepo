import { Component } from '@angular/core';
import { Register } from '../../../../models/Register';
import { UserService } from '../../../../services/UserService/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.scss'
})
export class CreateUserComponent {
  user: Register = {} as Register;
  selectedFile: File | null = null;
  errorMessage: string | null = null;
  isDragging = false;

  constructor(private userService: UserService, private router: Router) {}


  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const validTypes = ['image/png', 'image/jpeg', 'image/gif'];
      const maxSize = 10 * 1024 * 1024; // 10MB

      if (!validTypes.includes(file.type)) {
        this.errorMessage = 'Invalid file type. Please upload a PNG, JPG, or GIF.';
        return;
      }

      if (file.size > maxSize) {
        this.errorMessage = 'File is too large. Maximum size is 10MB.';
        return;
      }

      this.selectedFile = file;
      this.errorMessage = null;
    }
  }

  onRegister() {
    if (!this.selectedFile) {
      this.errorMessage = 'Nenhum arquivo selecionado.';
      return;
    }

    this.userService.register(this.user, this.selectedFile).subscribe(
      (response) => {
        console.log('Usuário criado com sucesso!', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Erro ao criar usuário:', error);
        this.errorMessage = error.message;
      }
    );
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.onFileSelected({ target: { files: files } });
    }
  }
}
