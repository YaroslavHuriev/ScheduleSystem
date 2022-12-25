import { Box, Container, Fab, Stack } from "@mui/material";
import SliderWithInput from "./SliderWithInput";
import axios from './AxiosInterceptor'
import { useEffect, useState } from "react";
import SaveIcon from '@mui/icons-material/Save';

export default function Settings(props) {
    const fabStyle = {
        margin: 0,
        top: 'auto',
        right: 20,
        bottom: 20,
        left: 'auto',
        position: 'fixed',
    };
    const [settings, setSettings] = useState({
        groupWindowPenalty: 0,
        lateLessonPenalty: 0,
        latestHour: 0,
        maxIterations: 0,
        maxOccurrencesOfOneDisciplinePerDayForGroup: 0,
        populationCount: 0,
        teacherWindowPenalty: 0,
        tooMuchOccurrencesOfOneDisciplinePerDayPenalty: 0
    });
    useEffect(() => {
        axios.get('api/settings').then((response) => {
            setSettings(response.data);
            console.log(settings)
        })
    }, [])
    const saveSettings = () => {
        axios.put('api/settings', settings).then((response) => {
            console.log(response)
        });
    };
    const handleMaxIterationsSliderChange = (event, newValue) => {
        setSettings({ ...settings, maxIterations: newValue });
    };
    const handlePopulationsSliderChange = (event, newValue) => {
        setSettings({ ...settings, populationCount: newValue });
    };
    const handleLatestHourSliderChange = (event, newValue) => {
        setSettings({ ...settings, latestHour: newValue - 1 });
    };
    const handleMaxSameDisciplineSliderChange = (event, newValue) => {
        setSettings({ ...settings, maxOccurrencesOfOneDisciplinePerDayForGroup: newValue });
    };
    const handleGroupWindowPenaltySliderChange = (event, newValue) => {
        setSettings({ ...settings, groupWindowPenalty: newValue });
    };
    const handleTeacherWindowPenaltySliderChange = (event, newValue) => {
        setSettings({ ...settings, teacherWindowPenalty: newValue });
    };
    const handleLateLessonsPenaltySliderChange = (event, newValue) => {
        setSettings({ ...settings, lateLessonPenalty: newValue });
    };
    const handleSameDisciplinePenaltySliderChange = (event, newValue) => {
        setSettings({ ...settings, tooMuchOccurrencesOfOneDisciplinePerDayPenalty: newValue });
    };

    return (
        <div><Box display="flex"
            justifyContent="center"
            alignItems="center">
            <Stack>
                <SliderWithInput
                    label='Максимальна кількість ітерацій'
                    onChange={handleMaxIterationsSliderChange}
                    value={settings.maxIterations}
                    min={100}
                    max={10000}
                    step={100}
                />
                <SliderWithInput
                    label='Кількість популяцій'
                    onChange={handlePopulationsSliderChange}
                    value={settings.populationCount}
                    min={100}
                    max={10000}
                    step={40}
                />
                <SliderWithInput
                    label='Номер пари, після якої інші пари ставити незручно'
                    onChange={handleLatestHourSliderChange}
                    value={settings.latestHour + 1}
                    min={1}
                    max={6}
                    step={1}
                />
                <SliderWithInput
                    label='Максимальна кількість появ однієї дисципліни в день у групи'
                    onChange={handleMaxSameDisciplineSliderChange}
                    value={settings.maxOccurrencesOfOneDisciplinePerDayForGroup}
                    min={1}
                    max={6}
                    step={1}
                />
                <SliderWithInput
                    label='Штраф за вікна у групи'
                    onChange={handleGroupWindowPenaltySliderChange}
                    value={settings.groupWindowPenalty}
                    min={0}
                    max={20}
                    step={1}
                />
                <SliderWithInput
                    label='Штраф за вікна у викладача'
                    onChange={handleTeacherWindowPenaltySliderChange}
                    value={settings.teacherWindowPenalty}
                    min={0}
                    max={20}
                    step={1}
                />
                <SliderWithInput
                    label='Штраф за пізні пари'
                    onChange={handleLateLessonsPenaltySliderChange}
                    value={settings.lateLessonPenalty}
                    min={0}
                    max={20}
                    step={1}
                />
                <SliderWithInput
                    label='Штраф за велику кількість однотипних пар в день'
                    onChange={handleSameDisciplinePenaltySliderChange}
                    value={settings.tooMuchOccurrencesOfOneDisciplinePerDayPenalty}
                    min={0}
                    max={20}
                    step={1}
                />
            </Stack>
        </Box>
            <Fab style={fabStyle} onClick={saveSettings} color="primary" aria-label="add">
                <SaveIcon />
            </Fab>
        </div>)
}