import {  Button, Dialog, DialogFooter, H5, Section, SectionCard} from'@blueprintjs/core'
import './NewCompanyWizardComponent.scss'
import {DialogBody} from '@fluentui/react-components'


export default function NewCompanyDialogComponent ({isOpen, toggleDialog})
{
	return <>
		<Dialog isOpen={isOpen}>
		<DialogBody>
			<Section className="newCompanyForm-container" >
				<SectionCard>
					<H5>New Company Registration</H5>
				</SectionCard>
			</Section>
		</DialogBody>
		<DialogFooter>
				<Button onClick={toggleDialog }>Close</Button>
		</DialogFooter>
	</Dialog>
	</>
}