import { useLocation, useNavigate } from "react-router";
import { Avatar, Caption2Strong, makeStyles, SelectTabData, SelectTabEvent, Spinner, Tab, TabList, tokens } from "@fluentui/react-components";
import { borders, breakpoints } from "../../../strings/ui";
import { Depths } from "@fluentui/react";
import { TbShieldLock, TbUserEdit, TbTrash, TbMail } from 'react-icons/tb'
import { useEffect, useState } from "react";
import { AppRoutes } from "../../../strings";
import { IAppUser } from "../../../types/identity";
import useIdentityManager from "../../../hooks/IdentityManager";
import PersonalInfoFragment from "./PersonalInfoFragment";
import ContactInfoFragment from "./ContactInfoFragment";
import ChangePasswordFragment from "./ChangePasswordFragment";
import ResetPasswordFragment from "./ResetPasswordFragment";
import AccountDeletionFragment from "./AccountDeletionFragment";

export interface IAccountFragmentProps {
    user?: IAppUser
}

const getFragmentData = () => ({
    [AppRoutes.Account.Profile]: {
        element: <PersonalInfoFragment />,
        title: "Personal Information",
    },
    [AppRoutes.Account.ContactInfo]: {
        element: <ContactInfoFragment />,
        title: "Contact Information",
    },
    [AppRoutes.Account.ChangePassword]: {
        element: <ChangePasswordFragment />,
        title: "Change Password",
    },
    [AppRoutes.Account.ResetPassword]: {
        element: <ResetPasswordFragment />,
        title: "Reset Password",
    },
    [AppRoutes.Account.Deletion]: {
        element: <AccountDeletionFragment />,
        title: "Account Deletion",
    },
})

export default function AccountPage() {
    const styles = MakeAccountPageStyles()
    const navigate = useNavigate()
    const location = useLocation()
    const [currentTab, setCurrentTab] = useState<string>(location.pathname)
    const identityManager = useIdentityManager()
    const user = identityManager.getUser()

    function onTabSelect(_event: SelectTabEvent, data: SelectTabData) {
        setCurrentTab(data.value as string)
    }

    useEffect(() => {
        if (location.pathname !== currentTab)
            navigate(currentTab!)
    }, [currentTab, location, navigate])

    return (!user ? <Spinner /> :
        <div className={`${styles.root} user_profile_layout`}>
            <div className={styles.contentContainer}>
                <div className={styles.accountNavBar}>
                    <div className={styles.avatarWrapper}>
                        <Avatar size={72} color="neutral" name={user.firstName + " " + user.lastName} />
                    </div>
                    <TabList appearance="filled-circular" size="small" onTabSelect={onTabSelect} selectedValue={location.pathname}>
                        <Tab as="button" icon={<TbUserEdit />} value={AppRoutes.Account.Profile}>Personal Info</Tab>
                        <Tab as="button" icon={<TbMail />} value={AppRoutes.Account.ContactInfo}>Contact Info</Tab>
                        <Tab as="button" icon={<TbShieldLock />} value={AppRoutes.Account.ChangePassword}>Change Password</Tab>
                        {/* <Tab as="button" icon={<TbShieldLock />} value={AppRoutes.Account.ResetPassword}>Reset Password</Tab> */}
                        <Tab as="button" icon={<TbTrash />} value={AppRoutes.Account.Deletion}>Account Deletion</Tab>
                    </TabList>
                </div>
                <div className={styles.outletContainer}>
                    <div className={styles.fragmentOutletWrapper}>
                        {getFragmentData()[currentTab]?.element}
                    </div>
                </div>
                <div className={styles.accountSummary}>
                    <Caption2Strong>Date Joined: {user.dateRegistered}</Caption2Strong>
                    <Caption2Strong>|</Caption2Strong>
                    <Caption2Strong>Username: {user.email}</Caption2Strong>
                </div>
            </div>
        </div>)
}

const MakeAccountPageStyles = makeStyles({
    root: {
        width: "100%",
        minHeight: "100%",
        padding: tokens.spacingHorizontalM,
        display: "flex",
        flexDirection: "column"
    },
    contentContainer: {
        width: breakpoints.medium,
        margin: "0 auto",
        display: "flex",
        flexDirection: "column",
        borderRadius: tokens.borderRadiusMedium,
        boxShadow: Depths.depth4,
        overflow: "hidden",
        backgroundColor: tokens.colorNeutralBackground1,
    },
    accountNavBar: {
        width: "100%",
        // paddingTop: tokens.spacingHorizontalM,
        // paddingLeft: tokens.spacingHorizontalM,
        // paddingRight: tokens.spacingHorizontalM,
        //boxShadow: Depths.depth4,
        padding: tokens.spacingHorizontalM,
        backgroundColor: tokens.colorNeutralBackground1,
        borderBottom: borders.thinNeutralLight,
        display: "flex",
        flexDirection: "column",
        alignItems: "center"
    },
    outletContainer: {
        width: "100%",
        minHeight: breakpoints.small,
        padding: tokens.spacingHorizontalM
        //padding: tokens.spacingHorizontalM
    },
    fragmentTitleWrapper: {
        display: 'flex',
        width: "100%",
        borderBottom: borders.thinNeutralLight,
        //padding: `0 0 ${tokens.spacingHorizontalM} 0`,
        padding: tokens.spacingHorizontalS + " " + tokens.spacingHorizontalM,
        backgroundColor: tokens.colorNeutralBackground1Hover,
        flexDirection: "row",
        justifyContent: "center"
    },
    accountSummary: {
        marginTop: "auto",
        padding: tokens.spacingHorizontalM,
        backgroundColor: tokens.colorNeutralBackground1Hover,
        display: "flex",
        justifyContent: "flex-end",
        flexDirection: "row",
        columnGap: tokens.spacingHorizontalM,
        pointerEvents: "none"
    },
    fragmentOutletWrapper: {
        padding: tokens.spacingHorizontalM
    },
    avatarWrapper: {
        margin: tokens.spacingVerticalS,
        marginBottom: tokens.spacingVerticalXL
    }
})