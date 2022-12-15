import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import { Link } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import * as yup from 'yup';
import axios from 'axios';
import { ErrorSnackBar } from './ErrorSnackbar';

const theme = createTheme();

const validationSchema = yup.object({
    userName: yup
        .string('Введіть логін')
        .required("Логін обов'язковий"),
    password: yup
        .string('Введіть пароль')
        .required("Пароль обов'язковий")
});

export default function SignIn() {
    const navigate = useNavigate();

    const formik = useFormik({
        initialValues: {
            userName: '',
            password: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            axios.post('api/login', values).then(response => {
                console.log(response)
                var token = response.data;
                localStorage.setItem("token", token);
                localStorage.setItem("username", values.userName);
                navigate('/schedule')
            })
                .catch(err => {
                    if (err.response.status === 401) {
                        setSnackbarState({ open: true, message: 'Помилка: неправильний логін або пароль.' })
                    };
                });
        },
    });

    const [snackbarState, setSnackbarState] = React.useState({
        open: false,
        message: ''
    });

    const handleSnackbarClose = (event, reason) => {
        setSnackbarState({
            ...snackbarState,
            open: false
        });
    };

    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Вхід
                    </Typography>
                    <Box component="form" onSubmit={formik.handleSubmit} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="userName"
                            label="Логін"
                            name="userName"
                            autoComplete="email"
                            autoFocus
                            value={formik.values.userName}
                            onChange={formik.handleChange}
                            error={formik.touched.userName && Boolean(formik.errors.userName)}
                            helperText={formik.touched.userName && formik.errors.userName}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Пароль"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            value={formik.values.password}
                            onChange={formik.handleChange}
                            error={formik.touched.password && Boolean(formik.errors.password)}
                            helperText={formik.touched.password && formik.errors.password}
                        />
                        <FormControlLabel
                            control={<Checkbox value="remember" color="primary" />}
                            label="Запам'ятати мене"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Увійти
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link to='/signup' variant="body2">
                                    {"Не маєте акаунта? Реєстрація"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
                <ErrorSnackBar
                    open={snackbarState.open}
                    message={snackbarState.message}
                    handleClose={handleSnackbarClose}
                ></ErrorSnackBar>
            </Container>
        </ThemeProvider>
    );
}