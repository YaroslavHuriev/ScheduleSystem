import { Alert, Snackbar } from '@mui/material';
import React from 'react';

export function ErrorSnackBar(props) {
    return (<Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        open={props.open}
        autoHideDuration={6000}
        onClose={props.handleClose}
    >
        <Alert onClose={props.handleClose} severity='error' sx={{ width: '100%' }}>
            {props.message}
        </Alert>
    </Snackbar>)
}