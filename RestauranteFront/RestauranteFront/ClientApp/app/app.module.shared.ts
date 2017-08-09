import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { Restaurante } from './components/restaurante/restaurante.component';
import { EditRestaurante } from './components/restaurante/edit.restaurante.component';
import { Prato } from './components/prato/prato.component';
import { EditPrato } from './components/prato/edit.prato.component';
import { ConfirmationDialogComponent } from './components/confirm/confirmation-dialog.component';
import { ContactComponent } from './components/contact/contact.component';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        Restaurante,
        EditRestaurante,
        Prato,
        EditPrato,
        ConfirmationDialogComponent,
        ContactComponent
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'restaurante', component: Restaurante },
            { path: 'editrestaurante/:id', component: EditRestaurante },
            { path: 'prato', component: Prato },
            { path: 'editprato/:id', component: EditPrato },
            { path: 'confirmation/:id/:caller', component: ConfirmationDialogComponent },
            { path: 'contact', component: ContactComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    entryComponents: [
        ConfirmationDialogComponent
    ]
};
