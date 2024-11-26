import { Button, Caption1Strong, makeStyles, tokens } from "@fluentui/react-components";
import { usePageTitle } from "../../hooks";
import { BsPlus } from "react-icons/bs";
import { FaStore } from "react-icons/fa";
import Borders from "../../strings/ui/Borders";
import { useNavigate } from "react-router";
import { AppRoutes } from "../../strings";

export function HomePage() {
	usePageTitle("App")
	const styles = MakeHomePageStyles()
	const navigate = useNavigate()

	return <div className={styles.homeRoot}>
		<div className={styles.companyListWrapper}>
			<div className={styles.CompanyListTitleBlock}>
				<FaStore size={22} /> Companies
			</div>
			<div className={styles.CompanyListContent}>
				<Caption1Strong>No Companies Listed...</Caption1Strong>
				{/* <Button appearance="transparent" disabled>No Companies Listed...</Button> */}
			</div>
			<div className={styles.companyListControls}>
				<Button onClick={() => navigate(AppRoutes.Companies.Create)} appearance="primary" icon={<BsPlus />}>Create Company</Button>
				{/* <CompoundButton appearance="primary" size="small" icon={<BsPlus />} secondaryContent="Create a new company.">Something New</CompoundButton> */}
			</div>
		</div>
	</div>
}


const MakeHomePageStyles = makeStyles({
	homeRoot: {
		display: "flex",
		justifyContent: "center",
		flexDirection: "row",
		padding: tokens.spacingVerticalXXXL,
		minHeight: "100%",
		height: "100%"
	},
	companyListWrapper: {
		padding: tokens.spacingVerticalS,
		width: "100%",
		height: "auto",
		display: "flex",
		flexDirection: "column",
		maxWidth: "576px",
		minHeight: "576px",
		rowGap: tokens.spacingVerticalM,
		borderRadius: tokens.borderRadiusMedium,
		backgroundColor: tokens.colorNeutralBackground1,
	},
	CompanyListTitleBlock: {
		//backgroundColor: tokens.colorBrandBackground2Hover,
		padding: tokens.spacingVerticalS + " 0 ",
		color: tokens.colorBrandBackground4Static,
		//borderRadius: tokens.borderRadiusMedium,
		fontSize: tokens.fontSizeBase400,
		fontWeight: tokens.fontWeightBold,
		display: "flex",
		flexDirection: "row",
		columnGap: tokens.spacingHorizontalM,
		borderBottom: Borders.thinNeutralLighter,
	},
	companyListControls: {
		borderTop: Borders.thinNeutralLighter,
		paddingTop: tokens.spacingVerticalS,
		display: "flex",
		flexDirection: "row",
		justifyContent: "flex-end",
		marginTop: "auto",
	},
	CompanyListContent: {
		minHeight: "200px",
		height: "auto",
		padding: `${tokens.spacingVerticalS} 0`
	}
})