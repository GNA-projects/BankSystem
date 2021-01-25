import React from 'react'
import {AdminPanel} from './style'

export default function Admin() {
    return (
        <AdminPanel>
            <AdminPanel.Link to='/create/user' heading='Create User'/>
            <AdminPanel.Link to='/delete/user' heading='Delete User'/>
            <AdminPanel.Link to='/create/account' heading='Create Account'/>
            <AdminPanel.Link to='/delete/account' heading='Delete Account'/>
        </AdminPanel>
    )
}
