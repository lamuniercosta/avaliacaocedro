import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { PratoService, IPrato, PratoModel } from './prato.model';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../confirm/confirmation-dialog.component';
   

@Component({
    selector: "prato",
    templateUrl: 'prato.component.html'
})

export class Prato implements OnInit {

    model = new PratoModel(0, 0, '', 0);
    Prato: IPrato;
    message: string;

    constructor(private service: PratoService, private router: Router) {

    }
    public pratos: IPrato[];

    ngOnInit(): void {
        this.service.LoadData().then(data => this.pratos = data);
    }

    GetPratos(name: string) {
        
    }

    EditPrato(id: number) {
        this.router.navigate(['/editprato/', id]);
    }

    DeletePrato(id: number) {
        this.router.navigate(['/confirmation/', id, 'prato']);
    }
}
