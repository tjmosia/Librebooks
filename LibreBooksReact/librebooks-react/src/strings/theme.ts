import {type Theme, createLightTheme, createDarkTheme, type BrandVariants} from '@fluentui/react-components'

const myNewTheme: BrandVariants = {
    10: "#020304",
    20: "#11191C",
    30: "#192A2F",
    40: "#1E373F",
    50: "#24444E",
    60: "#29525F",
    70: "#2D606F",
    80: "#326E80",
    90: "#367D92",
    100: "#3B8CA4",
    110: "#3F9CB7",
    120: "#56AAC4",
    130: "#75B7CD",
    140: "#90C4D6",
    150: "#AAD1DF",
    160: "#C4DFE9"
};

const lightTheme: Theme = {
    ...createLightTheme(myNewTheme),
    borderRadiusSmall: "0px",
    borderRadiusMedium: "2px",
};

const darkTheme: Theme = {
    ...createDarkTheme(myNewTheme),
};

darkTheme.colorBrandForeground1 = myNewTheme[110];
darkTheme.colorBrandForeground2 = myNewTheme[120];

export {
    darkTheme,
    lightTheme,
}