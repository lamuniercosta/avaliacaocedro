import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { PratoService, IPrato, PratoModel } from './prato.model';
import { IRestaurante } from '../restaurante/restaurante.model';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    selector: "editPrato",
    templateUrl: 'edit.prato.component.html',
    providers: [PratoService]
})

export class EditPrato implements OnInit {
    model: PratoModel;
    id: number;
    message: string;
    restaurantes: IRestaurante[];

    constructor(private service: PratoService, private route: ActivatedRoute, private router: Router)
    { }


    ngOnInit(): void {
        this.id = this.route.snapshot.params['id'];
        this.service.GetRestaurantes().then(data => this.restaurantes = data);

        if (this.id > 0) {
            this.model = this.service.GetPrato(this.id).then(model => this.model = model);
        }
        else {
            this.model = new PratoModel(0, 0, '', 0);
        }
        
    }

    SavePrato() {
        this.service.SavePrato(this.id, this.model).subscribe((response) => {
            if (response.status == 200) {
                this.goBack()
            }
            else {
                this.message = '<div class="alert alert-danger" role="alert">' + response.text()+'</div>';
            }
           });
    }

    goBack(): void {
        this.router.navigate(['/prato']);
    }
}