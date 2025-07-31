import { Component, OnInit } from '@angular/core';
import { CreateRealEstateAgent, Languages } from '../../../../../../models/CreateRealEstateAgent';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RealEstateAgentService } from '../../../../../../services/RealEstateAgent/real-estate-agent.service';
import { Router } from '@angular/router';
import { UserModel } from '../../../../../../models/UserModel';
import { environment } from '../../../../../../../../environment';
import { UserService } from '../../../../../../services/UserService/user.service';
interface AgentFormGroup {
  userId: FormControl<string | null>;
  officeEmail: FormControl<string | null>;
  totalSales: FormControl<number | null>;
  totalListings: FormControl<number | null>;
  certifications: FormControl<string[] | null>;
  languagesSpoken: FormControl<Languages[] | null>;
  [key: string]: AbstractControl;
}
@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrl: './create-agent.component.scss'
})

export class CreateAgentComponent implements OnInit {
  agentForm!: FormGroup;
  errorMessage: string | null = null;
  languagesOptions = Object.values(Languages);
  loggedUserDetails: UserModel | null = null;
  searchTerm: string = '';
  filteredUsers: UserModel[] = [];
  selectedUser: UserModel | null = null;
  showCreateSuccessAlert: boolean = false;
  certificationInput = new FormControl('');
  certifications: string[] = [];
  certificationOptions = "REALTOR®, CRS, ABR, SRS, GRI, CRP, SRES, GREEN";
  certificationsList: string[] = [];
  certificationInputs: string[] = [];

  constructor(
    private fb: FormBuilder,
    private realEstateAgentService: RealEstateAgentService,
    private userService: UserService,
    private router: Router
  ) {
    this.initializeForm();
    this.certificationsList = this.certificationOptions.split(', ');
  }

  private initializeForm() {
    this.agentForm = this.fb.group({
      userId: [''],
      officeEmail: ['', [Validators.required, Validators.email]],
      totalSales: [0, [Validators.required, Validators.min(0)]],
      totalListings: [0, [Validators.required, Validators.min(0)]],
      certifications: [[]],
      languagesSpoken: [[]]
    });
  }

  ngOnInit() {
    this.loadLoggedUserDetails();
  }

  onCertificationChange(event: any) {
    const certification = event.target.value;
    const isChecked = event.target.checked;
    const currentCertifications = this.agentForm.get('certifications')?.value || [];

    if (isChecked) {
      this.agentForm.patchValue({
        certifications: [...currentCertifications, certification]
      });
    } else {
      this.agentForm.patchValue({
        certifications: currentCertifications.filter((cert: string) => cert !== certification)
      });
    }
  }
  searchUsers() {
    if (this.searchTerm.length >= 2) {
      this.userService.getAllUsers().subscribe({
        next: (users) => {
          this.filteredUsers = users
            .filter(user =>
              `${user.firstName} ${user.lastName}`.toLowerCase().includes(this.searchTerm.toLowerCase()) &&
              user.access !== 'Admin' &&
              user.access !== 'Agent'
            )
            .map(user => ({
              id: user.id,
              firstName: user.firstName,
              lastName: user.lastName,
              email: user.email,
              access: user.access,
              imageUrl: user.imageUrl || '',
              phoneNumber: user.phoneNumber ? Number(user.phoneNumber) : undefined,
              upload: ''
            } as UserModel));
        },
        error: (error) => {
          console.error('Error searching users:', error);
          this.errorMessage = 'Error searching users';
        }
      });
    } else {
      this.filteredUsers = [];
    }
  }

  selectUser(user: UserModel) {
    this.selectedUser = user;
    this.agentForm.patchValue({ userId: user.id });
    this.filteredUsers = [];
    this.searchTerm = `${user.firstName} ${user.lastName}`;
  }

  onLanguageChange(event: any) {
    const language = event.target.value;
    const isChecked = event.target.checked;
    const currentLanguages = this.agentForm.get('languagesSpoken')?.value || [];

    if (isChecked) {
      this.agentForm.patchValue({
        languagesSpoken: [...currentLanguages, language]
      });
    } else {
      this.agentForm.patchValue({
        languagesSpoken: currentLanguages.filter((lang: string) => lang !== language)
      });
    }
  }

    // Add these methods
    addCertification() {
      const certification = this.certificationInput.value?.trim();
      if (certification) {
        this.certifications.push(certification);
        this.agentForm.patchValue({
          certifications: this.certifications
        });
        this.certificationInput.reset();
      }
    }

    removeCertification(index: number) {
      this.certifications.splice(index, 1);
      this.agentForm.patchValue({
        certifications: this.certifications
      });
    }
    updateCertificationCount(event: any) {
      const count = parseInt(event.target.value);
      this.certificationInputs = new Array(count).fill('');

      const formControls: { [key: string]: AbstractControl } = {
        userId: this.fb.control(this.agentForm.get('userId')?.value),
        officeEmail: this.fb.control(this.agentForm.get('officeEmail')?.value),
        totalSales: this.fb.control(this.agentForm.get('totalSales')?.value),
        totalListings: this.fb.control(this.agentForm.get('totalListings')?.value),
        languagesSpoken: this.fb.control(this.agentForm.get('languagesSpoken')?.value),
        certifications: this.fb.control([])
      };

      for (let i = 0; i < count; i++) {
        const controlName = `certification${i}`;
        formControls[controlName] = this.fb.control('');
      }

      this.agentForm = this.fb.group(formControls);
    }


    updateCertifications() {
      const validCertifications = this.certificationInputs.filter(cert => cert.trim() !== '');
      this.agentForm.patchValue({
        certifications: validCertifications
      });
    }
    onCreateAgent() {
      if (this.agentForm.valid && this.selectedUser) {
        const certifications = Object.keys(this.agentForm.controls)
          .filter(key => key.startsWith('certification'))
          .map(key => this.agentForm.get(key)?.value)
          .filter(cert => cert && typeof cert === 'string' && cert.trim().length > 0);

        const agentData: CreateRealEstateAgent = {
          userId: this.selectedUser.id,
          officeEmail: this.agentForm.get('officeEmail')?.value || '',
          totalSales: Number(this.agentForm.get('totalSales')?.value),
          totalListings: Number(this.agentForm.get('totalListings')?.value),
          certifications: certifications,
          languagesSpoken: this.agentForm.get('languagesSpoken')?.value || []
        };

        this.realEstateAgentService.createAgent(agentData).subscribe({
          next: (response) => {
            this.showCreateSuccessAlert = true;
            setTimeout(() => {
              this.showCreateSuccessAlert = false;
              this.router.navigate(['/back-office/admin/dashboard']);
            }, 3000);
          },
          error: (error) => {
            console.error('Error creating agent:', error);
            this.errorMessage = 'Failed to create agent. Please try again.';
          }
        });
      }
    }

  private loadLoggedUserDetails() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userService.retrieve(loggedUser.id).subscribe({
        next: (userDetails) => {
          this.loggedUserDetails = userDetails;
        },
        error: (error) => {
          console.error('Error loading logged user details:', error);
        }
      });
    }
  }
}
