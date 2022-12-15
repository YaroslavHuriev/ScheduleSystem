import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import { Link, useNavigate } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useFormik } from 'formik';
import * as yup from 'yup';
import axios from './AxiosInterceptor'

const theme = createTheme();

const validationSchema = yup.object({
    email: yup
        .string('Введіть адресу електронної пошти')
        .email('Введіть коректну адресу електронної пошти')
        .required("Електронна адреса обов'язкова"),
    password: yup
        .string('Введіть пароль')
        .min(8, 'Пароль має бути довжиною не менше 8 символів')
        .required("Пароль обов'язковий"),
    confirmpassword: yup
        .string()
        .required('Паролі мають співпадати')
        .oneOf([yup.ref('password')], 'Паролі мають співпадати')
});

export default function SignUp() {
    const navigate = useNavigate();
    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
            confirmpassword: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            axios.post('api/users', { username: values.email, password: values.password }).then(response => {
                console.log(response)
                navigate('/login')
            })
                .catch(err => console.log(err));
        },
    });

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
                        Реєстрація
                    </Typography>
                    <Box component="form" noValidate onSubmit={formik.handleSubmit} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    id="email"
                                    label="Електронна адреса"
                                    name="email"
                                    autoComplete="email"
                                    value={formik.values.email}
                                    onChange={formik.handleChange}
                                    error={formik.touched.email && Boolean(formik.errors.email)}
                                    helperText={formik.touched.email && formik.errors.email}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    name="password"
                                    label="Пароль"
                                    type="password"
                                    id="password"
                                    autoComplete="new-password"
                                    value={formik.values.password}
                                    onChange={formik.handleChange}
                                    error={formik.touched.password && Boolean(formik.errors.password)}
                                    helperText={formik.touched.password && formik.errors.password}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    name="confirmpassword"
                                    label="Підтвердження пароля"
                                    type="password"
                                    id="confirmpassword"
                                    value={formik.values.confirmpassword}
                                    onChange={formik.handleChange}
                                    error={formik.touched.confirmpassword && Boolean(formik.errors.confirmpassword)}
                                    helperText={formik.touched.confirmpassword && formik.errors.confirmpassword}
                                />
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Зареєструватися
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link to='/login' variant="body2">
                                    Уже маєте акаунт? Увійти
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}