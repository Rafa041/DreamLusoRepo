import { Component } from '@angular/core';
import { Languages } from '../../../../../models/CreateRealStateAgent';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RealStateAgentService } from '../../../../../services/RealStateAgent/real-state-agent.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrl: './create-agent.component.scss'
})
export class CreateAgentComponent {
  agentForm: FormGroup; // Mudar de 'agent' para 'agentForm'
  selectedFile: File | null = null;
  errorMessage: string | null = null;
  isDragging = false;
  languagesOptions = Object.values(Languages);

  constructor(
    private realStateAgentService: RealStateAgentService,
    private router: Router,
    private fb: FormBuilder // Adicionando FormBuilder
  ) {
    // Inicializando o FormGroup
    this.agentForm = this.fb.group({
      userId: ['', Validators.required],
      officeEmail: ['', [Validators.required, Validators.email]],
      totalSales: ['', Validators.required],
      totalListings: ['', Validators.required],
      certifications: [[]], // Assumindo que será um array
      languagesSpoken: [[]], // Assumindo que será um array
    });
  }

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

  onLanguageChange(event: any) {
    const selectedLanguage = event.target.value as Languages; // O tipo já está correto
    if (event.target.checked) {
      this.agentForm.get('languagesSpoken')?.value.push(selectedLanguage);
    } else {
      const currentLanguages: Languages[] = this.agentForm.get('languagesSpoken')?.value; // Definindo o tipo aqui
      this.agentForm.get('languagesSpoken')?.setValue(
        currentLanguages.filter((lang: Languages) => lang !== selectedLanguage) // Definindo o tipo aqui
      );
    }
  }

  onCreateAgent() {
    if (!this.selectedFile) {
      this.errorMessage = 'Nenhum arquivo selecionado.';
      return;
    }

    const formData = new FormData();
    const agentData = this.agentForm.value; // Obtendo dados do FormGroup

    formData.append('userId', agentData.userId);
    formData.append('officeEmail', agentData.officeEmail);
    formData.append('totalSales', agentData.totalSales.toString());
    formData.append('totalListings', agentData.totalListings.toString());
    formData.append('certifications', agentData.certifications.join(','));
    formData.append('languagesSpoken', agentData.languagesSpoken.join(','));

    this.realStateAgentService.createAgent(formData).subscribe(
      (response) => {
        console.log('Agente imobiliário criado com sucesso!', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Erro ao criar agente:', error);
        this.errorMessage = 'Erro ao criar o agente imobiliário. Por favor, tente novamente.';
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
