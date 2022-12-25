import * as React from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Slider from '@mui/material/Slider';
import MuiInput from '@mui/material/Input';

const Input = styled(MuiInput)`
  width: 42px;
`;

export default function SliderWithInput(props) {

    return (
        <Box sx={{ width: 250 }}>
            <Typography id="input-slider" gutterBottom>
                {props.label}
            </Typography>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs>
                    <Slider
                        value={typeof props.value === 'number' ? props.value : 0}
                        step={props.step}
                        min={props.min}
                        max={props.max}
                        onChange={props.onChange}
                        valueLabelDisplay="auto"
                        aria-labelledby="input-slider"
                    />
                </Grid>
            </Grid>
        </Box>
    );
}