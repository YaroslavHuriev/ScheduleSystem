import { Alert, Snackbar } from '@mui/material';
import React, { useEffect, useState } from 'react';

export function SuccessSnackBar(props) {
    return (<Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        open={props.open}
        autoHideDuration={6000}
        onClose={props.handleClose}
    >
        <Alert onClose={props.handleClose} severity="success" sx={{ width: '100%' }}>
            {props.message}
        </Alert>
    </Snackbar>)
}