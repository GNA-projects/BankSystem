import React from 'react'
import {AdminPanel} from './style'

export default function Admin() {
    return (
        <AdminPanel>
            <AdminPanel.Link to='/admin/create/user' heading='Create User'/>
            <AdminPanel.Link to='/admin/create/baccount' heading='Create Bank Account'/>
            <AdminPanel.Link to='/admin/create/credit' heading='Create Credit Account'/>
            <AdminPanel.Link to='/admin/create/deposit' heading='Create Deposit Account'/>
            <AdminPanel.Link to='/admin/create/wallet' heading='Create Wallet'/>
            <AdminPanel.Link to='/admin/create/debit' heading='Create Debit Card'/>
            <AdminPanel.Link to='/admin/delete/user' heading='Delete User' red/>
            <AdminPanel.Link to='/admin/delete/account' heading='Delete Account' red/>
            <AdminPanel.Link to='/admin/delete/debit' heading='Delete Debit Card' red/>
        </AdminPanel>
    )
}
